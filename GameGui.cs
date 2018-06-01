using Godot;
using GodotCSTools;

namespace Simon {
    public class GameGui : Container
    {
        [NodePath(nameof(ScoreLabel))] protected Label ScoreLabel;
        
        public int Score;

        public override void _Ready() {
            this.SetupNodeTools();
        }

        public void IncrementScore() {
            Score++;
            ScoreLabel.Text = Score.ToString();
        }
    }
}
