using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace PowerSourceControlApp.DeviceManagment
{
    public class StatusChecker
    {
        private PowerSource.PowerSource ParentPowerSource { get; }
        private byte[] _message;
        private uint _errorCounter;
        private readonly int _bufferSize;
        private readonly Random _randomNumberGenerator;
        private Thread _chekThread;
        private TcpClient _statusSocket;
        private NetworkStream _statusStream;
        

        public StatusChecker(PowerSource.PowerSource parent)
        {
            _randomNumberGenerator = new Random();
            ParentPowerSource = parent;
            _bufferSize = 64;
        }

        public void Start()
        {
            ParentPowerSource.IsOnline = true;
            _statusSocket = new TcpClient();
            var threadName = string.Concat("StatusCheker:", ParentPowerSource.IpAddress);
            _chekThread = new Thread(StatusChekerThread)
            {
                Name = threadName,
                IsBackground = true,
                Priority = ThreadPriority.Normal
            };
            _errorCounter = 0;
            _chekThread.Start();
        }

        private void StatusChekerThread()
        {
            var incomingbuffer = new byte[_bufferSize];            
            var watch = System.Diagnostics.Stopwatch.StartNew();

            while (ParentPowerSource.IsOnline)
            {
                if (ParentPowerSource.Message == "")
                {
                    _message = Encoding.UTF8.GetBytes("What your state?");
                }
                else
                {
                    _message = Encoding.UTF8.GetBytes(ParentPowerSource.Message);
                    ParentPowerSource.Message = "";
                }

                Thread.Sleep(_randomNumberGenerator.Next(100, 200));

                if (!_statusSocket.Connected) // Connection does not exist
                {
                    try // Create new connection
                    {
                        _statusSocket.Connect(ParentPowerSource.IpAddress, ParentPowerSource.StatusPort);
                        _statusSocket.ReceiveTimeout = 100;
                        _errorCounter = 0;
                    }
                    catch (Exception) // Wait for an error
                    {
                        _errorCounter++;
                    }
                }
                else
                {
                    if ((_statusStream == null) || (_statusStream.CanRead == false && _statusStream.CanWrite == false)) // Stream does not exist
                    {
                        try // Create new stream
                        {
                            _statusStream = _statusSocket.GetStream();
                            _errorCounter = 0;
                        }
                        catch (SocketException) // Wait for an error
                        {
                            _errorCounter++;
                        }
                    }
                    else
                    {
                        try
                        {
                            _statusStream.Write(_message, 0, _message.Length);
                        }
                        catch (Exception)
                        {
                            _errorCounter++;
                        }
                        try
                        {
                            Array.Clear(incomingbuffer, 0, _bufferSize);
                            _statusStream.Read(incomingbuffer, 0, _bufferSize);
                            if (incomingbuffer.Length > 0 && incomingbuffer.ToString() != "")
                            {
                                ParentPowerSource.Status = Encoding.UTF8.GetString(incomingbuffer).Replace("\0", "");
                                _errorCounter = 0;
                                watch.Restart();
                            }
                            else
                            {
                                _errorCounter++;
                            }
                        }
                        catch (Exception)
                        {
                            _errorCounter++;
                        }
                    }
                }
                var time = watch.ElapsedMilliseconds;
                if ((_errorCounter > 4)||(time > 1000))
                {
                    while (DeviceManager.IsBusy)
                    {
                        Thread.Sleep(1);
                    }
                    DeviceManager.IsBusy = true;
                    ParentPowerSource.IsOnline = false;
                    if (_statusStream != null)
                    {
                        _statusStream.Close();
                        _statusStream.Dispose();
                    }
                    if (_statusSocket != null)
                    {
                        _statusSocket.Close();
                        _statusSocket.Dispose();
                    }
                    GC.Collect();
                    watch.Stop();
                    DeviceManager.IsBusy = false;
                    return;
                }
            }
        }
    }
}
