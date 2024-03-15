using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace roguegame
{
    enum Species
    {
        Duck,
        Mongoose,
        Elf
    }

    enum Role
    {
        Cook,
        Smith,
        Rogue
    }
    internal class PlayerCharacter
    {
        public string Name;
        public Species Species;
        public Role Role;
        public Vector2 Position;
    }

}
