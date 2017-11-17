using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PowerSourceControlApp.DeviceManagment
{
    public class StatusChecker
    {
        private PowerSource ParentPowerSource { get; }
        private uint _errorCounter;
        private readonly int _bufferSize;
        private readonly Random _randomNumberGenerator;
        private Thread _chekThread;
        private TcpClient _statusSocket;
        private NetworkStream _statusStream;
        

        public StatusChecker(PowerSource parent)
        {
            _randomNumberGenerator = new Random();
            ParentPowerSource = parent;
            _bufferSize = 1024;
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
            };
            _errorCounter = 0;
            _chekThread.Start();
        }

        private void StatusChekerThread()
        {
             
            var incomingbuffer = new byte[_bufferSize];
            var message = Encoding.UTF8.GetBytes("What your state?");


            while (ParentPowerSource.IsOnline)
            {
                Thread.Sleep(100 + _randomNumberGenerator.Next(0, 100));

                if (!_statusSocket.Connected) // Connection does not exist
                {
                    try // Create new connection
                    {
                        _statusSocket.Connect(ParentPowerSource.IpAddress, ParentPowerSource.StatusPort);
                        _errorCounter = 0;
                    }
                    catch (Exception e) // Wait for an error
                    {
                        _errorCounter++;
                    }
                }
                else
                {
                    if ((_statusStream == null) || (_statusStream.CanRead == false && _statusStream.CanWrite == false)) // Stream does not exist
                    {
                        var huy = 0;
                        try // Create new stream
                        {
                            _statusStream = _statusSocket.GetStream();
                            _errorCounter = 0;
                        }
                        catch (SocketException e) // Wait for an error
                        {
                            _errorCounter++;
                        }
                    }
                    else
                    {
                        try
                        {
                            _statusStream.Write(message, 0, message.Length);
                        }
                        catch (Exception e)
                        {
                            _errorCounter++;
                        }
                        try
                        {
                            Array.Clear(incomingbuffer, 0, _bufferSize);
                            _statusStream.Read(incomingbuffer, 0, _bufferSize);
                            if (incomingbuffer.Length > 0 && incomingbuffer.ToString() != "")
                            {
                                ParentPowerSource.Status = Encoding.UTF8.GetString(incomingbuffer);
                                _errorCounter = 0;
                            }
                            else
                            {
                                _errorCounter++;
                            }
                        }
                        catch (Exception e)
                        {
                            _errorCounter++;
                        }
                    }
                }

                if (_errorCounter > 4)
                {
                    ParentPowerSource.IsOnline = false;
                    ParentPowerSource.Collection.isUpdated = true;
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
                    return;
                }
            }
        }
    }
}
