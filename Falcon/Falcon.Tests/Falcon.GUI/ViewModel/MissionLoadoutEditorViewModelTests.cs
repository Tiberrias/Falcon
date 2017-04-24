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
        private Mock<IVirtualArsenalLoadoutService>_virtualArsenalLoadoutService;

        [SetUp]
        public void SetUp()
        {
            _virtualArsenalLoadoutService = new Mock<IVirtualArsenalLoadoutService>();
            _sut = new MissionLoadoutEditorViewModel(_virtualArsenalLoadoutService.Object);
        }

    }
}