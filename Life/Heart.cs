using Godot;
using GodotCSTools;

namespace Simon.Life {
    public class Heart : Node2D
    {
        [NodePath(nameof(HeartSprite))] protected Sprite HeartSprite;
        [NodePath(nameof(CrossSprite))] protected Sprite CrossSprite;

        private bool _damaged;

        [Export]
        public bool Damaged {
            get => _damaged;
            set {
                _damaged = value; 
                UpdateSprites();
            }
        }

        public override void _Ready() {
            this.SetupNodeTools();

            UpdateSprites();
        }

        private void UpdateSprites() {
            if (HeartSprite == null) return;
            
            HeartSprite.Visible = !Damaged;
            CrossSprite.Visible = Damaged;
        }
    }
}
