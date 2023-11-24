using PrototypeWPF.Operations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototypeWPF
{
    public class EditorViewModel
    {
        public ObservableCollection<IOperation> Nodes { get; } = new ObservableCollection<IOperation> ();

        public EditorViewModel()
        {
            Nodes.Add(new Blur());
        }
    }
}
