using Godot;
using GodotCSTools;

namespace Simon {
    public class FeedbackAudio : Node
    {
        [NodePath(nameof(SuccessAudio))] protected AudioStreamPlayer SuccessAudio;
        [NodePath(nameof(FailAudio))] protected AudioStreamPlayer FailAudio;

        public override void _Ready() {
            this.SetupNodeTools();
        }

        public void PlaySuccess() {
            SuccessAudio.Playing = true;
        }

        public void PlayFailure() {
            FailAudio.Playing = true;
        }
    }
}
