using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanFolder.Model.Interfaces
{
    public interface IPersistentObject {
        void Save();

        void Load();
    }
}
