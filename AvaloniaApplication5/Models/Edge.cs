namespace AvaloniaApplication5.Models
{
    public class Edge
    {
        public Node From { get; set; }
        public Node To { get; set; }
        public double RestLength { get; set; } = 100;
        public int TotalPackets { get; set; } = 0;
        public int LostPackets { get; set; }
        public ProtocolType Protocol { get; set; } = ProtocolType.Other;
        public double LossRate =>
            TotalPackets == 0 ? 0 : (double) LostPackets / TotalPackets;
        public uint? LastSeq { get; set; }

        public Edge(Node from, Node to)
        {
            From = from;
            To = to;
        }
    }
}