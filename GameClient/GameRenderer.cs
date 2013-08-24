﻿using System.Collections.Generic;
using System.Drawing;
using BattleCity.GameClient.GUI;
using BattleCity.GameLib;
using BattleCity.GameLib.Tanks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using BattleCity.GraphicsLib;

namespace BattleCity.GameClient
{
    /// <summary>
    /// Class, which is responsible for displaying the gameplay
    /// </summary>
    internal class GameRenderer
    {//think, we can load textures in Renderer instead of passing them as parameter
        public GameRenderer(float windowWidth, float windowHeight, Texture[] mapTextureList, Texture[] tankTextureList)
        {
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
            this.mapTextureList = mapTextureList;
            this.tankTextureList = tankTextureList;
            elementWidth = windowWidth / 19;
            elementHeight = windowHeight / 20;
        }

        public float WindowWidth 
        {
            get { return windowWidth; }
        }

        public float WindowHeight
        {
            get { return windowHeight; }
        }

        public float ElementWidth
        {
            get { return elementWidth; }
        }

        public float ElementHeight
        {
            get { return elementHeight; }
        }

        private float windowWidth, windowHeight;
        private float elementWidth, elementHeight;
        private Texture[] mapTextureList;
        private Texture[] tankTextureList;

        private void DrawTexture(int x, int y, Texture texture)
        {
            Vector2 v1 = new Vector2(x, y);
            Vector2 v2 = new Vector2(x + elementWidth, y);
            Vector2 v3 = new Vector2(x + elementWidth, y - elementHeight);
            Vector2 v4 = new Vector2(x, y - elementHeight);
            //Clear zone
            GL.Begin(BeginMode.Quads);
            {
                GL.Color3(Color.Black);
                GL.Vertex2(v1);
                GL.Vertex2(v2);
                GL.Vertex2(v3);
                GL.Vertex2(v4);
            }
            GL.End();

            //mapping texture
            texture.Bind();
            GL.Begin(BeginMode.Quads);
            {
                GL.Color4(Color4.Transparent);
                GL.TexCoord2(0, 0);
                GL.Vertex2(v1);
                GL.TexCoord2(1, 0);
                GL.Vertex2(v2);
                GL.TexCoord2(1, 1);
                GL.Vertex2(v3);
                GL.TexCoord2(0, 1);
                GL.Vertex2(v4);
            }
            GL.End();
        }

        #region Map Methods

        public void DrawMap(MapObject[][] map)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(Color.Black);
            for (int i = 0; i < map.Length; i++)
                for (int j = 0; j < map[i].Length; j++)
                    switch (map[i][j].Type)
                    {
                        case MapObject.Types.EMPTY: DrawMapPart(i, j, 0);
                            break;
                        case MapObject.Types.BRICK: DrawMapPart(i, j, 1);
                            break;
                        case MapObject.Types.CONCRETE: DrawMapPart(i, j, 2);
                            break;
                        case MapObject.Types.WATER: DrawMapPart(i, j, 3);
                            break;
                        case MapObject.Types.FOREST: DrawMapPart(i, j, 4);
                            break;
                        case MapObject.Types.BASE: DrawMapPart(i, j, 5);
                            break;
                    }
        }

        public void DrawMapPart(int x, int y, MapObject.Types type)
        {
            switch (type)
            {
                case MapObject.Types.EMPTY: DrawMapPart(x, y, 0);
                    break;
                case MapObject.Types.BRICK: DrawMapPart(x, y, 1);
                    break;
                case MapObject.Types.CONCRETE: DrawMapPart(x, y, 2);
                    break;
                case MapObject.Types.WATER: DrawMapPart(x, y, 3);
                    break;
                case MapObject.Types.FOREST: DrawMapPart(x, y, 4);
                    break;
                case MapObject.Types.BASE: DrawMapPart(x, y, 5);
                    break;
            }
        }

        private void DrawMapPart(int x, int y, int textureIndex)
        {
            Vector2 v1 = new Vector2(-windowWidth / 2 + y * elementWidth, windowHeight / 2 - x * elementHeight);
            Vector2 v2 = new Vector2(-windowWidth / 2 + y * elementWidth + elementWidth,
                                   windowHeight / 2 - x * elementHeight);
            Vector2 v3 = new Vector2(-windowWidth / 2 + y * elementWidth + elementWidth,
                           windowHeight / 2 - x * elementHeight - elementHeight);
            Vector2 v4 = new Vector2(-windowWidth / 2 + y * elementWidth, windowHeight / 2 - x * elementHeight - elementHeight);
            //Clear zone
            GL.Begin(BeginMode.Quads);
            {
                GL.Color3(Color.Black);
                GL.Vertex2(v1);
                GL.Vertex2(v2);
                GL.Vertex2(v3);
                GL.Vertex2(v4);
            }
            GL.End();

            //mapping texture
            mapTextureList[textureIndex].Bind();
            GL.Begin(BeginMode.Quads);
            {
                GL.Color4(Color4.Transparent);
                GL.TexCoord2(0, 0);
                GL.Vertex2(v1);
                GL.TexCoord2(1, 0);
                GL.Vertex2(v2);
                GL.TexCoord2(1, 1);
                GL.Vertex2(v3);
                GL.TexCoord2(0, 1);
                GL.Vertex2(v4);
            }
            GL.End();
        }

        #endregion Map Methods

        #region Tank Methods
        
        public void DrawTanks(IList<AbstractTank> tanks)
        {
            foreach (var tank in tanks)
            {
                switch (tank.type)
                {
                    case AbstractTank.Type.PlayerNormal:
                        DrawTexture(tank.X, tank.Y, tankTextureList[0]);
                        break;
                    case AbstractTank.Type.PlayerFast:
                        break;
                }
            }
        }

        public void DrawTank(int x, int y, AbstractTank.Type type)
        {
            switch (type)
            {
                case AbstractTank.Type.PlayerNormal:
                    DrawTexture(x, y, tankTextureList[0]);
                    //DrawTank(x, y, 0);
                    break;
                case AbstractTank.Type.PlayerFast:
                    break;
            }
        }

        #endregion Tank Methods

        #region Bullet Methods

        #endregion Bullet Methods

        #region Bonus's Methods

        #endregion Bonus's Methods
    }
}