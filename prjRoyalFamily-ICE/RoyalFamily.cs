using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjRoyalFamily_ICE
{
    class RoyalFamily
    {
        public RoyalFamilyMember Member { get; set; }
        public List<RoyalFamily> Children { get; set; }

        public RoyalFamily(RoyalFamilyMember member)
        {
            Member = member;
            Children = new List<RoyalFamily>();
        }

        // Adds a child to the node
        public void AddChild(RoyalFamily child)
        {
            Children.Add(child);
        }
    }
}
