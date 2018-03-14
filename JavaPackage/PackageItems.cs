using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaPackage
{
    public class PackageItems
    {
        private ObservableCollection<MavenElement> _getMavenElement;

        public ObservableCollection<MavenElement> GetMavenElement { get => _getMavenElement; set => _getMavenElement = value; }
    }
}
