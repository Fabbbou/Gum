﻿using Gum.DataTypes;
using Gum.Managers;
using GumRuntime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameGum.Renderables;
using RenderingLibrary;
using System.Linq;

namespace MonoGameGumFromFile
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            SystemManagers.Default = new SystemManagers();
            SystemManagers.Default.Initialize(_graphics.GraphicsDevice, fullInstantiation: true);
            ElementSaveExtensions.CustomCreateGraphicalComponentFunc = RenderableCreator.HandleCreateGraphicalComponent;

            StandardElementsManager.Self.Initialize();

            var gumProject = GumProjectSave.Load("Content/GumProject.gumx", out _);
            ObjectFinder.Self.GumProjectSave = gumProject;
            gumProject.Initialize();

            var item = gumProject.Screens.First().ToGraphicalUiElement(SystemManagers.Default, true);


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            SystemManagers.Default.Activity(gameTime.TotalGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SystemManagers.Default.Draw();

            base.Draw(gameTime);
        }
    }
}
