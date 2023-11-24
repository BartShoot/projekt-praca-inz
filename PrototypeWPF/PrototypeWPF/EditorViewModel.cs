using PrototypeWPF.Operations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototypeWPF
{
    public class ConnectorViewModel
    {
        public string Title { get; set; }
    }
    public class NodeViewModel
    {
        public string Title { get; set; }
        public ObservableCollection<ConnectorViewModel> Input { get; set; } = new ObservableCollection<ConnectorViewModel>();
        public ObservableCollection<ConnectorViewModel> Output { get; set; } = new ObservableCollection<ConnectorViewModel>();
    }
    public class EditorViewModel
    {
        public ObservableCollection<NodeViewModel> Nodes { get; } = new ObservableCollection<NodeViewModel> ();

        public EditorViewModel()
        {
            Nodes.Add(new NodeViewModel
            {
                Title = "Welcome",
                Input = new ObservableCollection<ConnectorViewModel>
                {
                    new ConnectorViewModel
                    {
                        Title = "In"
                    }
                },
                Output = new ObservableCollection<ConnectorViewModel>
                {
                    new ConnectorViewModel
                    {
                        Title = "Out"
                    }
                }
            });
        }
    }
}
