using Falcon.Core.Services.Interfaces;
using Falcon.GUI.ViewModel;
using Moq;
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