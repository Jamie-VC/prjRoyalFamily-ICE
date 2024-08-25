using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjRoyalFamily_ICE
{
    class Tree
    {
        public RoyalFamily Root { get; set; }

        public Tree()
        {
            InitializeTree();
        }

        private void InitializeTree()
        {
            var queenElizabeth = new RoyalFamilyMember("Queen Elizabeth II", new DateTime(1926, 4, 21), false);
            Root = new RoyalFamily(queenElizabeth);

            var princeCharles = new RoyalFamily(new RoyalFamilyMember("Prince Charles", new DateTime(1948, 11, 14), true));
            var princessAnne = new RoyalFamily(new RoyalFamilyMember("Princess Anne", new DateTime(1950, 8, 15), true));
            var princeAndrew = new RoyalFamily(new RoyalFamilyMember("Prince Andrew", new DateTime(1960, 2, 19), true));
            var princeEdward = new RoyalFamily(new RoyalFamilyMember("Prince Edward", new DateTime(1964, 3, 10), true));

            Root.AddChild(princeCharles);
            Root.AddChild(princessAnne);
            Root.AddChild(princeAndrew);
            Root.AddChild(princeEdward);
        }

        public RoyalFamily FindMember(RoyalFamily currentNode, string name)
        {
            if (currentNode.Member.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                return currentNode;
            }

            foreach (var child in currentNode.Children)
            {
                var result = FindMember(child, name);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        public int GetPositionInLine(RoyalFamily node)
        {
            int position = 1;
            var queue = new Queue<RoyalFamily>();
            queue.Enqueue(Root);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (current == node)
                {
                    return position;
                }

                foreach (var child in current.Children)
                {
                    queue.Enqueue(child);
                }

                position++;
            }

            return -1;
        }
    }
}
