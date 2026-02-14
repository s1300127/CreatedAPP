using AvaloniaApplication5.Models;
using PacketDotNet;
using SharpPcap;
using SharpPcap.LibPcap;
using SkiaSharp;
using System.Collections.Generic;
using System.Linq;

namespace AvaloniaApplication5.Services
{
    public static class PcapParser{
        public static  ParsedGraph Parse(string filePath)
        {
            var reader = new CaptureFileReaderDevice(filePath);
            reader.Open();

            var nodeDict = new Dictionary<string, Node>();
            var edges = new List<Edge>();
            var edgeDict = new Dictionary<EdgeKey, Edge>();

            PacketCapture capture;
            while(reader.GetNextPacket(out capture) == GetPacketStatus.PacketRead)
            {
                var raw = capture.GetPacket();
                var packet = Packet.ParsePacket(raw.LinkLayerType, raw.Data);
                var ip = packet.Extract<IPPacket>();
                var tcp = packet.Extract<TcpPacket>();
                var udp = packet.Extract<UdpPacket>();
                
                if(ip == null) continue;

                string src = ip.SourceAddress.ToString();
                string dst = ip.DestinationAddress.ToString();
                var key = new EdgeKey(src,dst);

                // management of Node using dictionary(prevent duplication)
                if(!nodeDict.ContainsKey(src))
                    nodeDict[src] = new Node(src, new System.Numerics.Vector2(0,0));

                if(!nodeDict.ContainsKey(dst))
                    nodeDict[dst] = new Node(dst, new System.Numerics.Vector2(0,0));
                
                if(!edgeDict.TryGetValue(key, out var edge))
                {
                    edge = new Edge(nodeDict[src], nodeDict[dst]);
                    if(tcp != null)
                        edge.Protocol = Models.ProtocolType.TCP;
                    else
                        edge.Protocol = Models.ProtocolType.Other;

                    edgeDict[key] = edge;
                }

                if(tcp != null)
                {
                    uint seq = tcp.SequenceNumber;
                    
                    if(edge.LastSeq != null)
                    {
                        uint expected = (uint)edge.LastSeq.Value + (uint)tcp.PayloadData.Length;
                        if (seq > expected)
                        {
                            edge.LostPackets += (int)(seq-expected);
                        }
                    }
                    edge.LastSeq=seq;
                }

                edge.TotalPackets++;
            }
            reader.Close();

            return new ParsedGraph(
                nodeDict.Values.ToArray(),
                edgeDict.Values.ToArray()
            );
        }
    }
}