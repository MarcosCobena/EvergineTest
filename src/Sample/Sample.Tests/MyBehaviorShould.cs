﻿using Evergine.Common.Input.Keyboard;
using Evergine.Common.Input.Mouse;
using Evergine.Framework;
using Evergine.Framework.Graphics;
using Evergine.Mocks;
using Xunit;

namespace Sample.Tests
{
    public class MyBehaviorShould
    {
        private readonly MyBehavior component;

        private readonly MockWindowsSystem windowsSystem;

        public MyBehaviorShould()
        {
            this.component = new MyBehavior();
            var entity = new Entity()
                .AddComponent(this.component)
                .AddComponent(new Camera3D())
                .AddComponent(new Transform3D());
            var scene = new MockScene();
            scene.Add(entity);
            var application = new MyApplication();
            this.windowsSystem = MockWindowsSystem.Create(application, scene);
        }

        [Fact]
        public void KeepMyBooleanPropertyFalseOnStart()
        {
            // Arrange

            // Act
            this.windowsSystem.RunOneLoop();

            // Assert
            Assert.False(this.component.MyBooleanProperty);
        }

        [Fact]
        public void ChangeMyBooleanPropertyToTrueAfterTwoLoops()
        {
            // Arrange

            // Act
            this.windowsSystem.RunOneLoop();
            this.windowsSystem.RunOneLoop();

            // Assert
            Assert.True(this.component.MyBooleanProperty);
        }

        [Fact]
        public void KeepEntityAtOriginWithoutPressingUpKey()
        {
            // Arrange

            // Act
            this.windowsSystem.RunOneLoop();
            this.windowsSystem.RunOneLoop();

            // Assert
            var transform3D = this.component.Owner.FindComponent<Transform3D>();
            Assert.Equal(0, transform3D.Position.Y);
        }

        [Fact]
        public void MoveEntityAfterPressingUpKey()
        {
            // Arrange

            // Act
            this.windowsSystem.RunOneLoop();
            this.windowsSystem.KeyboardDispatcher?.Press(Keys.Up);
            this.windowsSystem.RunOneLoop();

            // Assert
            var transform3D = this.component.Owner.FindComponent<Transform3D>();
            Assert.Equal(1, transform3D.Position.Y);
        }

        [Fact]
        public void KeepEntityAtOriginWithoutMovingMouseWithLeftButtonPressed()
        {
            // Arrange

            // Act
            this.windowsSystem.RunOneLoop();
            this.windowsSystem.RunOneLoop();

            // Assert
            var transform3D = this.component.Owner.FindComponent<Transform3D>();
            Assert.Equal(0, transform3D.Position.X);
        }

        [Fact]
        public void MoveEntityAfterMovingMouseWithLeftButtonPressed()
        {
            // Arrange
            var mouseDispatcher = this.windowsSystem.MouseDispatcher!;

            // Act
            this.windowsSystem.RunOneLoop();
            mouseDispatcher.Press(MouseButtons.Left);
            mouseDispatcher.Move(100, 0);
            this.windowsSystem.RunOneLoop();

            // Assert
            var transform3D = this.component.Owner.FindComponent<Transform3D>();
            Assert.Equal(1, transform3D.Position.X);
        }
    }
}
