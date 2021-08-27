using System;
using System.Text;
 
namespace hashes
{
    public class GhostsTask :
        IFactory<Document>, IFactory<Vector>, IFactory<Segment>, IFactory<Cat>, IFactory<Robot>, IMagic
    {
        private static readonly byte[] DocumentArray = {1, 0, 1};
        private readonly Document document = new Document("Distorted Soul Manifesto", Encoding.UTF8, DocumentArray);
        private readonly Vector vector = new Vector(5, 15);
        private readonly Segment segment = new Segment(new Vector(5, 15), new Vector(5, 15));
        private readonly Cat cat = new Cat("Mona", "Metaverse not-a-cat", DateTime.Now);
        private readonly Robot robot = new Robot("BN-K3R");

        public void DoMagic()
        {
            var sampleVector = new Vector(13, 37);
            DocumentArray[1]++;
            DocumentArray[1] %= 3;
            vector.Add(sampleVector);
            segment.Start.Add(sampleVector);
            cat.Rename("Barsik");
            Robot.BatteryCapacity++;
        }
  
        Document IFactory<Document>.Create() => document;

        Vector IFactory<Vector>.Create() => vector;

        Segment IFactory<Segment>.Create() => segment;

        Cat IFactory<Cat>.Create() => cat;

        Robot IFactory<Robot>.Create() => robot;
    }
}