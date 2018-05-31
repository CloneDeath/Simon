using System;
using Godot;
using GodotCSTools;
using Object = Godot.Object;

namespace Simon {
    public class Tile : Node2D
    {
        [Export] public TileColor Color = TileColor.Yellow;
        
        [NodePath("Sprites/BlueSprite")] protected Sprite BlueSprite;
        [NodePath("Sprites/GreenSprite")] protected Sprite GreenSprite;
        [NodePath("Sprites/RedSprite")] protected Sprite RedSprite;
        [NodePath("Sprites/YellowSprite")] protected Sprite YellowSprite;
        
        [NodePath("AnimationPlayer")] protected AnimationPlayer AnimationPlayer;

        public event Action<TileColor> Activated;

        public override void _Ready() {
            this.SetupNodeTools();
            UpdateColor();
        }

        private void UpdateColor() {
            BlueSprite.Visible = Color == TileColor.Blue;
            GreenSprite.Visible = Color == TileColor.Green;
            RedSprite.Visible = Color == TileColor.Red;
            YellowSprite.Visible = Color == TileColor.Yellow;
        }

        public void Demo() {
            AnimationPlayer.Play("Demo");
        }
        
        public void Hit() {
            AnimationPlayer.Play("Hit");
        }
        
        public void OnClickAreaInputEvent(Object viewport, Object @event, int shapeIdx)
        {
            if (@event is InputEventMouseButton mouse) {
                if (mouse.Pressed && mouse.ButtonIndex == 1) {
                    Activated?.Invoke(Color);
                    Hit();
                }
            }
        }
    }
}
