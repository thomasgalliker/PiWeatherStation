using System;
using System.Collections.Generic;
using System.Linq;
using DisplayService.Model;
using DisplayService.Resources;
using DisplayService.Services;
using FluentAssertions;
using Moq.AutoMock;
using Xunit;
using Xunit.Abstractions;

namespace DisplayService.Tests.Services
{
    public class RenderServiceTests
    {
        private readonly TestHelper testHelper;
        private readonly AutoMocker autoMocker;

        public RenderServiceTests(ITestOutputHelper testOutputHelper)
        {
            this.testHelper = new TestHelper(testOutputHelper);
            this.autoMocker = new AutoMocker();

            var renderSettingsMock = this.autoMocker.GetMock<IRenderSettings>();
            renderSettingsMock.SetupGet(r => r.Height)
                .Returns(480);
            renderSettingsMock.SetupGet(r => r.Width)
                .Returns(800);
            renderSettingsMock.SetupGet(r => r.BackgroundColor)
                .Returns(Colors.White);
        }

        [Fact]
        public void ShouldGetScreen_Empty()
        {
            // Arrange
            IRenderService renderService = this.autoMocker.CreateInstance<RenderService>();

            // Act
            var bitmapStream = renderService.GetScreen();

            // Assert
            bitmapStream.Should().NotBeNull();
            this.testHelper.WriteFile(bitmapStream);
        }

        [Fact]
        public void ShouldGetScreen_Text()
        {
            // Arrange
            IRenderService renderService = this.autoMocker.CreateInstance<RenderService>();

            var fontSize = 120;

            var textTopLeft = new RenderActions.Text
            {
                X = 0,
                Y = 0,
                VerticalTextAlignment = VerticalAlignment.Top,
                HorizontalTextAlignment = HorizontalAlignment.Left,
                Value = $"TL",
                ForegroundColor = Colors.Black,
                FontSize = fontSize,
            };
            renderService.Text(textTopLeft);

            var textTopCenter = new RenderActions.Text
            {
                X = 400,
                Y = 0,
                VerticalTextAlignment = VerticalAlignment.Top,
                HorizontalTextAlignment = HorizontalAlignment.Center,
                Value = $"TC",
                ForegroundColor = Colors.Black,
                FontSize = fontSize,
            };
            renderService.Text(textTopCenter);

            var textTopRight = new RenderActions.Text
            {
                X = 800,
                Y = 0,
                VerticalTextAlignment = VerticalAlignment.Top,
                HorizontalTextAlignment = HorizontalAlignment.Right,
                Value = $"TR",
                ForegroundColor = Colors.Black,
                FontSize = fontSize,
            };
            renderService.Text(textTopRight);

            var textCenterLeft = new RenderActions.Text
            {
                X = 0,
                Y = 240,
                VerticalTextAlignment = VerticalAlignment.Center,
                HorizontalTextAlignment = HorizontalAlignment.Left,
                Value = $"CL",
                ForegroundColor = Colors.Black,
                FontSize = fontSize,
            };
            renderService.Text(textCenterLeft);

            var textCenterCenter = new RenderActions.Text
            {
                X = 400,
                Y = 240,
                VerticalTextAlignment = VerticalAlignment.Center,
                HorizontalTextAlignment = HorizontalAlignment.Center,
                Value = $"CC",
                ForegroundColor = Colors.Black,
                FontSize = fontSize,
            };
            renderService.Text(textCenterCenter);

            var textCenterRight = new RenderActions.Text
            {
                X = 800,
                Y = 240,
                VerticalTextAlignment = VerticalAlignment.Center,
                HorizontalTextAlignment = HorizontalAlignment.Right,
                Value = $"CR",
                ForegroundColor = Colors.Black,
                FontSize = fontSize,
            };
            renderService.Text(textCenterRight);

            var textBottomLeft = new RenderActions.Text
            {
                X = 0,
                Y = 480,
                VerticalTextAlignment = VerticalAlignment.Bottom,
                HorizontalTextAlignment = HorizontalAlignment.Left,
                Value = $"BL",
                ForegroundColor = Colors.Black,
                FontSize = fontSize,
            };
            renderService.Text(textBottomLeft);

            var textBottomCenter = new RenderActions.Text
            {
                X = 400,
                Y = 480,
                VerticalTextAlignment = VerticalAlignment.Bottom,
                HorizontalTextAlignment = HorizontalAlignment.Center,
                Value = $"BC",
                ForegroundColor = Colors.Black,
                FontSize = fontSize,
            };
            renderService.Text(textBottomCenter);

            var textBottomRight = new RenderActions.Text
            {
                X = 800,
                Y = 480,
                VerticalTextAlignment = VerticalAlignment.Bottom,
                HorizontalTextAlignment = HorizontalAlignment.Right,
                Value = $"BR",
                ForegroundColor = Colors.Black,
                FontSize = fontSize,
            };
            renderService.Text(textBottomRight);

            // Act
            var bitmapStream = renderService.GetScreen();

            // Assert
            bitmapStream.Should().NotBeNull();
            this.testHelper.WriteFile(bitmapStream);
        }

        [Fact]
        public void ShouldGetScreen_Text2()
        {
            // Arrange
            IRenderService renderService = this.autoMocker.CreateInstance<RenderService>();

            var text = new RenderActions.Text
            {
                X = 0,
                Y = 0,
                Value = "Test",
                ForegroundColor = "FFFFFF",
                BackgroundColor = "000000",
                FontSize = 20,
            };
            renderService.Text(text);

            // Act
            var bitmapStream = renderService.GetScreen();

            // Assert
            bitmapStream.Should().NotBeNull();
            this.testHelper.WriteFile(bitmapStream);
        }

        [Fact]
        public void ShouldGetScreen_Text3()
        {
            // Arrange
            IRenderService renderService = this.autoMocker.CreateInstance<RenderService>();

            var dateTime = new DateTime(2022, 12, 31, 12, 00, 00, DateTimeKind.Local);

            var renderActions = new List<IRenderAction>
            {
                new RenderActions.Rectangle
                {
                    X = 0,
                    Y = 0,
                    Height = 100,
                    Width = 800,
                    BackgroundColor = "#000000",
                },
                new RenderActions.Text
                {
                    X = 20,
                    Y = 50,
                    HorizontalTextAlignment = HorizontalAlignment.Left,
                    VerticalTextAlignment = VerticalAlignment.Center,
                    Value = dateTime.ToString("dddd, d. MMMM"),
                    ForegroundColor = "#FFFFFF",
                    FontSize = 70,
                    AdjustsFontSizeToFitWidth = true,
                    AdjustsFontSizeToFitHeight = true,
                    Bold = true,
                },
                new RenderActions.Text
                {
                    X = 798,
                    Y = 88,
                    HorizontalTextAlignment = HorizontalAlignment.Right,
                    VerticalTextAlignment = VerticalAlignment.Top,
                    Value = $"v1.2.3-pre",
                    ForegroundColor = "#FFFFFF",
                    BackgroundColor = "#000000",
                    FontSize = 12,
                    Bold = false,
                },
            }.ToArray();

            renderService.Render(renderActions);

            // Act
            var bitmapStream = renderService.GetScreen();

            // Assert
            bitmapStream.Should().NotBeNull();
            this.testHelper.WriteFile(bitmapStream);
        }

        [Fact]
        public void ShouldGetScreen_Text_AdjustsFontSizeToFitWidth()
        {
            // Arrange
            IRenderService renderService = this.autoMocker.CreateInstance<RenderService>();

            var fontSize = 70;
            var numberOfValues = 20;

            var textWithFitWidth = new RenderActions.Text
            {
                X = 0,
                Y = 0,
                VerticalTextAlignment = VerticalAlignment.Top,
                HorizontalTextAlignment = HorizontalAlignment.Left,
                Value = string.Join("_", Enumerable.Range(1, numberOfValues)),
                ForegroundColor = Colors.Black,
                BackgroundColor = Colors.LightGray,
                FontSize = fontSize,
                AdjustsFontSizeToFitWidth = true,
            };
            renderService.Text(textWithFitWidth);

            var textWithoutFitWidth = new RenderActions.Text
            {
                X = 0,
                Y = 100,
                VerticalTextAlignment = VerticalAlignment.Top,
                HorizontalTextAlignment = HorizontalAlignment.Left,
                Value = string.Join("_", Enumerable.Range(1, numberOfValues)),
                ForegroundColor = Colors.Black,
                BackgroundColor = Colors.LightGray,
                FontSize = fontSize,
                AdjustsFontSizeToFitWidth = false,
            };
            renderService.Text(textWithoutFitWidth);

            // Act
            var bitmapStream = renderService.GetScreen();

            // Assert
            bitmapStream.Should().NotBeNull();
            this.testHelper.WriteFile(bitmapStream);
        }

        [Fact]
        public void ShouldGetScreen_Text_AdjustsFontSizeToFitHeight()
        {
            // Arrange
            IRenderService renderService = this.autoMocker.CreateInstance<RenderService>();

            var fontSize = 1000;
            var startAt = 8;
            var numberOfValues = 3;

            var textWithFitHeight = new RenderActions.Text
            {
                X = 0,
                Y = 0,
                VerticalTextAlignment = VerticalAlignment.Top,
                HorizontalTextAlignment = HorizontalAlignment.Left,
                Value = string.Join("_", Enumerable.Range(startAt, numberOfValues)),
                ForegroundColor = Colors.Black,
                BackgroundColor = Colors.LightGray,
                FontSize = fontSize,
                AdjustsFontSizeToFitHeight = true,
                AdjustsFontSizeToFitWidth = false,
            };
            renderService.Text(textWithFitHeight);

            // Act
            var bitmapStream = renderService.GetScreen();

            // Assert
            bitmapStream.Should().NotBeNull();
            this.testHelper.WriteFile(bitmapStream);
        }

        [Fact]
        public void ShouldGetScreen_Text_AdjustsFontSizeToFitWidthAndHeight()
        {
            // Arrange
            IRenderService renderService = this.autoMocker.CreateInstance<RenderService>();

            var fontSize = 1000;
            var startAt = 8;
            var numberOfValues = 3;

            var textWithFitHeight = new RenderActions.Text
            {
                X = 0,
                Y = 0,
                VerticalTextAlignment = VerticalAlignment.Top,
                HorizontalTextAlignment = HorizontalAlignment.Left,
                Value = string.Join("_", Enumerable.Range(startAt, numberOfValues)),
                ForegroundColor = Colors.Black,
                BackgroundColor = Colors.LightGray,
                FontSize = fontSize,
                AdjustsFontSizeToFitHeight = true,
                AdjustsFontSizeToFitWidth = true,
            };
            renderService.Text(textWithFitHeight);

            // Act
            var bitmapStream = renderService.GetScreen();

            // Assert
            bitmapStream.Should().NotBeNull();
            this.testHelper.WriteFile(bitmapStream);
        }

        [Fact]
        public void ShouldGetScreen_Rectangles()
        {
            // Arrange
            IRenderService renderService = this.autoMocker.CreateInstance<RenderService>();

            var rectangleTopLeft = new RenderActions.Rectangle
            {
                X = 0,
                Y = 0,
                Height = 80,
                Width = 200,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                BackgroundColor = Colors.Red,
            };
            renderService.Rectangle(rectangleTopLeft);

            var rectangleTopCenter = new RenderActions.Rectangle
            {
                X = 400,
                Y = 0,
                Height = 80,
                Width = 200,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Center,
                BackgroundColor = Colors.Red,
            };
            renderService.Rectangle(rectangleTopCenter);

            var rectangleTopRight = new RenderActions.Rectangle
            {
                X = 800,
                Y = 0,
                Height = 80,
                Width = 200,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Right,
                BackgroundColor = Colors.Red,
            };
            renderService.Rectangle(rectangleTopRight);

            var rectangleCenterLeft = new RenderActions.Rectangle
            {
                X = 0,
                Y = 240,
                Height = 80,
                Width = 200,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                BackgroundColor = Colors.Blue,
            };
            renderService.Rectangle(rectangleCenterLeft);

            var rectangleCenterCenter = new RenderActions.Rectangle
            {
                X = 400,
                Y = 240,
                Height = 80,
                Width = 200,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                BackgroundColor = Colors.Blue,
            };
            renderService.Rectangle(rectangleCenterCenter);

            var rectangleCenterRight = new RenderActions.Rectangle
            {
                X = 800,
                Y = 240,
                Height = 80,
                Width = 200,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Right,
                BackgroundColor = Colors.Blue,
            };
            renderService.Rectangle(rectangleCenterRight);

            var rectangleBottomLeft = new RenderActions.Rectangle
            {
                X = 0,
                Y = 480,
                Height = 80,
                Width = 200,
                VerticalAlignment = VerticalAlignment.Bottom,
                HorizontalAlignment = HorizontalAlignment.Left,
                BackgroundColor = Colors.Green,
            };
            renderService.Rectangle(rectangleBottomLeft);

            var rectangleBottomCenter = new RenderActions.Rectangle
            {
                X = 400,
                Y = 480,
                Height = 80,
                Width = 200,
                VerticalAlignment = VerticalAlignment.Bottom,
                HorizontalAlignment = HorizontalAlignment.Center,
                BackgroundColor = Colors.Green,
            };
            renderService.Rectangle(rectangleBottomCenter);

            var rectangleBottomRight = new RenderActions.Rectangle
            {
                X = 800,
                Y = 480,
                Height = 80,
                Width = 200,
                VerticalAlignment = VerticalAlignment.Bottom,
                HorizontalAlignment = HorizontalAlignment.Right,
                BackgroundColor = Colors.Green,
            };
            renderService.Rectangle(rectangleBottomRight);

            // Act
            var bitmapStream = renderService.GetScreen();

            // Assert
            bitmapStream.Should().NotBeNull();
            this.testHelper.WriteFile(bitmapStream);
        }

        [Fact]
        public void ShouldGetScreen_BitmapImage_TestImage1()
        {
            // Arrange
            IRenderService renderService = this.autoMocker.CreateInstance<RenderService>();

            var image = new RenderActions.BitmapImage
            {
                X = 0,
                Y = 0,
                Image = BitmapImages.GetTestImage1(),
            };
            renderService.Image(image);

            // Act
            var bitmapStream = renderService.GetScreen();

            // Assert
            bitmapStream.Should().NotBeNull();
            this.testHelper.WriteFile(bitmapStream);
        }

        [Fact]
        public void ShouldGetScreen_BitmapImage_TestImage2()
        {
            // Arrange
            IRenderService renderService = this.autoMocker.CreateInstance<RenderService>();

            var image = new RenderActions.BitmapImage
            {
                X = 0,
                Y = 0,
                Image = BitmapImages.GetTestImage2(),
            };
            renderService.Image(image);

            // Act
            var bitmapStream = renderService.GetScreen();

            // Assert
            bitmapStream.Should().NotBeNull();
            this.testHelper.WriteFile(bitmapStream);
        }

        [Fact]
        public void ShouldGetScreen_SvgImage_SvgImage1()
        {
            // Arrange
            IRenderService renderService = this.autoMocker.CreateInstance<RenderService>();

            var image = new RenderActions.SvgImage
            {
                X = 0,
                Y = 0,
                Image = SvgImages.GetSvgImage1(),
            };
            renderService.SvgImage(image);

            // Act
            var bitmapStream = renderService.GetScreen();

            // Assert
            bitmapStream.Should().NotBeNull();
            this.testHelper.WriteFile(bitmapStream);
        }
        
        [Fact]
        public void ShouldGetScreen_SvgImage_SvgImage1_Scaled()
        {
            // Arrange
            IRenderService renderService = this.autoMocker.CreateInstance<RenderService>();

            var image = new RenderActions.SvgImage
            {
                X = 100,
                Y = 100,
                Width = 100,
                Height = 100,
                Image = SvgImages.GetSvgImage1(),
                BackgroundColor = "00EE00"
            };
            renderService.SvgImage(image);

            // Act
            var bitmapStream = renderService.GetScreen();

            // Assert
            bitmapStream.Should().NotBeNull();
            this.testHelper.WriteFile(bitmapStream);
        }
        
        [Fact]
        public void ShouldGetScreen_SvgImage_SvgImage2_StrokeWidth()
        {
            // Arrange
            IRenderService renderService = this.autoMocker.CreateInstance<RenderService>();

            var image = new RenderActions.SvgImage
            {
                X = 100,
                Y = 100,
                Width = 100,
                Height = 100,
                Image = SvgImages.GetSvgImage2(),
                BackgroundColor = "00EE00"
            };
            renderService.SvgImage(image);

            // Act
            var bitmapStream = renderService.GetScreen();

            // Assert
            bitmapStream.Should().NotBeNull();
            this.testHelper.WriteFile(bitmapStream);
        }

        [Fact]
        public void ShouldGetScreen_StackLayout()
        {
            // Arrange
            IRenderService renderService = this.autoMocker.CreateInstance<RenderService>();

            var text1 = new RenderActions.Text
            {
                X = 0,
                Y = 0,
                Value = "80",
                ForegroundColor = "000000",
                BackgroundColor = "#f542ad",
                VerticalTextAlignment = VerticalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 70,
            };

            var text2 = new RenderActions.Text
            {
                X = 0,
                Y = 0,
                Value = "°C",
                ForegroundColor = "000000",
                BackgroundColor = "#42e6f5",
                VerticalAlignment = VerticalAlignment.Center,
                VerticalTextAlignment = VerticalAlignment.Bottom,
                FontSize = 35,
            };

            var stackLayout = new RenderActions.StackLayout
            {
                Width = 200,
                Height = 100,
                X = 400,
                Y = 240,
                Orientation = StackOrientation.Horizontal,
                Spacing = 6,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                BackgroundColor = "#e8e8e8"
            };
            stackLayout.Children.Add(text1);
            stackLayout.Children.Add(text2);

            renderService.StackLayout(stackLayout);

            // Act
            var bitmapStream = renderService.GetScreen();

            // Assert
            bitmapStream.Should().NotBeNull();
            this.testHelper.WriteFile(bitmapStream);
        }
    }
}
