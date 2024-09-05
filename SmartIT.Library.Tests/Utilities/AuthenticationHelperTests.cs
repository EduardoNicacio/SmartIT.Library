namespace SmartIT.Library.Tests.Utilities
{
	using SmartIT.Library.Utilities;

	[TestFixture]
	public class AuthenticationHelperTests
	{
		[SetUp]
		public void Setup()
		{
			// Method intentionally left empty.
		}

		[Test]
		public void Validate_AuthenticationHelper_GetWindowsUser()
		{
			// Arrange

			// Act
			var windowsUser = AuthenticationHelper.GetWindowsUser();

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(windowsUser, Is.Not.EqualTo(null));
				Assert.That(windowsUser.Key, Is.Not.EqualTo(null));
				Assert.That(windowsUser.Value, Is.Not.EqualTo(null));
			});
		}

		[Test]
		public async Task Validate_AuthenticationHelper_GetWindowsUserAsync()
		{
			// Arrange

			// Act
			var windowsUser = await AuthenticationHelper.GetWindowsUserAsync();

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(windowsUser, Is.Not.EqualTo(null));
				Assert.That(windowsUser.Key, Is.Not.EqualTo(null));
				Assert.That(windowsUser.Value, Is.Not.EqualTo(null));
			});
		}
	}
}
