using Moq;
using ppedv.Fuhrparkverwaltung.Model.Contracts;

namespace ppedv.Fuhrparkverwaltung.Logic.FuhrparkService.Tests
{
    public class DeviceManagerTests
    {
        [Fact]
        public void Device_cools_down_afert_overheating_event()
        {
            var mock = new Mock<IDevice>();
            var dm = new DeviceManager(mock.Object);
            var testAlarm = "AAAA";

            mock.Raise(x => x.Overheating += null, testAlarm);

            mock.Verify(x => x.Init("cooldown!" + testAlarm), Times.Once);
        }
    }
}
