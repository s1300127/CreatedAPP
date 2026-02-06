using SharpPcap;
using System;
using SharpPcap.LibPcap;
using PacketDotNet;

namespace AvaloniaApplication1{
    public static class Packetcap
    {
        public static string ReadPcapFile(string filePath)
        {
            string response = "";

            if(filePath == null)
            {
                response = "Please provide a valid pcap file path.";
                return response;
            }
            try{
                using (var device = new CaptureFileReaderDevice(filePath))
                {
                    device.OnPacketArrival += new PacketArrivalEventHandler((sender, e) => response = Device_OnPacketArrival(sender, e, response));

                    //response+="Starting packet read from file...";
                    device.Open();
                    device.Capture();
                    device.Close();
                    //response+="Finished reading packets.";
                }
                return response;
            }catch(Exception ex)
            {
                response = $"An error occurred: {ex.Message}";
                return response;
            }
        }

        private static string Device_OnPacketArrival(object sender, PacketCapture e, string output)
        {
            var rawPacket = e.GetPacket();
            var packet = PacketDotNet.Packet.ParsePacket(rawPacket.LinkLayerType, rawPacket.Data);

            var ethernetPacket = (EthernetPacket)packet;
            var ipPacket = (IPPacket)ethernetPacket.PayloadPacket;
            var tcpPacket = (TcpPacket)ipPacket.PayloadPacket;
            var ethernetPacketpayload = ethernetPacket.PayloadPacket;

            if (ethernetPacket != null)
            {
                output += $"Timestamp: {rawPacket.Timeval.Date}, Src MAC: {ethernetPacket.SourceHardwareAddress}, Dst MAC: {ethernetPacket.DestinationHardwareAddress}\n";
                output += $"IPLength: {ipPacket.PayloadLength} bytes\n";
                output += $"IPpayload: {ipPacket.PayloadPacket}\n";
                output += $"Src IP: {ipPacket.SourceAddress} -> Dst IP: {ipPacket.DestinationAddress}\n";
                output += $"TCPHeader: {tcpPacket.HeaderData.Length} bytes\n";
                output += $"DataLength: {tcpPacket.PayloadData.Length} bytes\n";
                output += $"data:\n {System.Text.Encoding.UTF8.GetString(tcpPacket.PayloadData)}\n";
                output += "-----------------------------------------------------\n";
            }
            return output;
        }
    }
}