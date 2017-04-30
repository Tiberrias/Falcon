using Falcon.GUI.ViewModel;
using NUnit.Framework;

namespace Falcon.Tests.Falcon.GUI.ViewModel
{
    [TestFixture]
    public class MissionLoadoutEditorViewModelTests
    {
        private MissionLoadoutEditorViewModel _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new MissionLoadoutEditorViewModel();
        }
    }
}