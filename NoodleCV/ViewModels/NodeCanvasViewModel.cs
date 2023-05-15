using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using NoodleCV.Models;
using NoodleCV.Views;
using ReactiveUI;

namespace NoodleCV.ViewModels
{
	public class NodeCanvasViewModel : ViewModelBase
	{
		public NodeCanvasViewModel() 
		{
			AddNode = ReactiveCommand.Create(AddNodeTest);
		}
		public ReactiveCommand<Unit,Unit> AddNode { get; }
		void AddNodeTest()
		{
			Node newNode = new Node();
		}
	}
}
