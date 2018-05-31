using System;
using System.Collections.Generic;
using Godot;
using GodotCSTools;

namespace Simon {
    public class Board : Node2D
    {
        [NodePath("Tiles/RedTile")] protected Tile RedTile;
        [NodePath("Tiles/BlueTile")] protected Tile BlueTile;
        [NodePath("Tiles/GreenTile")] protected Tile GreenTile;
        [NodePath("Tiles/YellowTile")] protected Tile YellowTile;

        private static readonly Random _random = new Random();
        
        private readonly List<TileColor> _order = new List<TileColor>();

        private readonly List<TileColor> _inputQueue = new List<TileColor>();
        
        public override void _Ready() {
            this.SetupNodeTools();
            
            RedTile.Activated += OnTileActivated;
            BlueTile.Activated += OnTileActivated;
            GreenTile.Activated += OnTileActivated;
            YellowTile.Activated += OnTileActivated;
            
            AddOrder();
            Playback();
        }

        private void OnTileActivated(TileColor color) {
            _inputQueue.Add(color);
            CheckForCompletion();
        }

        private void CheckForCompletion() {
            if (_inputQueue.Count < _order.Count) return;
            
            AddOrder();
            Playback();
        }

        public void Playback() {
            DisableInput();
            _inputQueue.Clear();
            for (var i = 0; i < _order.Count; i++) {
                QueuePlaybackFor(i);
            }
            
            var timer = new Timer {
                OneShot = true,
                WaitTime = _order.Count
            };
            AddChild(timer);
            
            timer.Connect("timeout", this, nameof(EnableInput));
            timer.Connect("timeout", timer, "queue_free");
            
            timer.Start();
        }

        private void QueuePlaybackFor(int i) {
            var timer = new Timer {
                OneShot = true,
                WaitTime = i + 1
            };
            AddChild(timer);

            var tile = GetTileForOrder(i);
            
            timer.Connect("timeout", tile, nameof(Tile.Demo));
            timer.Connect("timeout", timer, "queue_free");
            
            timer.Start();
        }

        private Tile GetTileForOrder(int i) {
            var color = _order[i];
            return GetTileForColor(color);
        }

        private Tile GetTileForColor(TileColor color) {
            switch (color) {
                case TileColor.Blue: return BlueTile;
                case TileColor.Green: return GreenTile;
                case TileColor.Red: return RedTile;
                case TileColor.Yellow: return YellowTile;
            }

            throw new Exception();
        }

        public void DisableInput() {
            RedTile.InputEnabled = false;
            BlueTile.InputEnabled = false;
            GreenTile.InputEnabled = false;
            YellowTile.InputEnabled = false;
        }
        
        public void EnableInput() {
            RedTile.InputEnabled = true;
            BlueTile.InputEnabled = true;
            GreenTile.InputEnabled = true;
            YellowTile.InputEnabled = true;
        }

        private void AddOrder() {
            _order.Add(GetRandomColor());
        }

        private TileColor GetRandomColor() {
            return (TileColor)_random.Next(4);
        }
    }
}
