using SmartIT.Library.Helpers;

namespace SmartIT.Library.Tests.Helpers
{
	[TestFixture]
	public class StreamHelperTests
	{
		[SetUp]
		public void Setup()
		{
			// Method intentionally left empty.
		}

		[Test]
		public void Verify_StreamToByteArray_ValidInput()
		{
			// Arrange
			var buffer = new byte[4096];
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = StreamHelper.StreamToByteArray(memoryStream);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(4096));
		}

		[Test]
		public async Task Verify_StreamToByteArrayAsync_ValidInput()
		{
			// Arrange
			var buffer = new byte[4096];
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = await StreamHelper.StreamToByteArrayAsync(memoryStream);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(4096));
		}

		[Test]
		public void Verify_StreamToByteArray_EmptyInput()
		{
			// Arrange
			var buffer = new byte[0];
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = StreamHelper.StreamToByteArray(memoryStream);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(0));
		}

		[Test]
		public async Task Verify_StreamToByteArrayAsync_EmptyInput()
		{
			// Arrange
			var buffer = new byte[0];
			var memoryStream = new MemoryStream(buffer);

			// Act
			var result = await StreamHelper.StreamToByteArrayAsync(memoryStream);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result, Has.Length.EqualTo(0));
		}

		[Test]
		public void Verify_StreamToByteArray_NullInputThrowsArgumentNullException()
		{
			// Arrange

			// Act

			// Assert
			Assert.Throws<ArgumentNullException>(() => StreamHelper.StreamToByteArray(null));
		}

		[Test]
		public Task Verify_StreamToByteArrayAsync_NullInputThrowsArgumentNullException()
		{
			// Arrange

			// Act

			// Assert
			Assert.ThrowsAsync<ArgumentNullException>(async () => await StreamHelper.StreamToByteArrayAsync(null));
			return Task.CompletedTask;
		}
	}
}
