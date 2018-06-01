using System.Collections.Generic;
using Godot;

namespace Simon.Life {
    public class LifeBar : Node2D
    {
        [Export] public int MaxHealth = 3;

        [Signal] public delegate void NoMoreHealth();

        public int CurrentHealth { get; private set; }
        
        protected PackedScene HeartScene = (PackedScene)ResourceLoader.Load("res://Life/Heart.tscn");

        private readonly List<Heart> _hearts = new List<Heart>(); 

        public override void _Ready() {
            CurrentHealth = MaxHealth;
            
            for (var i = 0; i < MaxHealth; i++) {
                var heart = (Heart)HeartScene.Instance();
                heart.Position = new Vector2(0, i * 200);
                AddChild(heart);
                _hearts.Add(heart);
            }
        }

        public void TakeDamage() {
            _hearts[_hearts.Count - CurrentHealth].Damaged = true;
            CurrentHealth--;

            if (CurrentHealth <= 0) EmitSignal(nameof(NoMoreHealth));
        }
    }
}
