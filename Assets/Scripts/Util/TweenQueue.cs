using System.Collections.Generic;

using DG.Tweening;

namespace CodeBlaze.Detris.Util {

    public class TweenQueue {

        private Queue<Sequence> _sequenceQueue;

        public TweenQueue() {
            _sequenceQueue = new Queue<Sequence>();
        }

        public void Add(Tween tween) {
            // Create a paused DOTween sequence to "wrap" our tween
            var sequence = DOTween.Sequence();
            sequence.Pause();
            // "Wrap" the tween
            sequence.Append(tween);
            // Add tween to queue
            _sequenceQueue.Enqueue(sequence);

            // If this is the only tween in queue, play it immediately
            if (_sequenceQueue.Count == 1) {
                _sequenceQueue.Peek().Play();
            }

            // When the tween finishes, we'll evaluate the queue
            sequence.OnComplete(OnComplete);
        }

        private void OnComplete() {
            // Tween completed. Remove it.
            _sequenceQueue.Dequeue();

            // Other tweens awaiting?
            if (_sequenceQueue.Count > 0) {
                // Play next tween in queue
                _sequenceQueue.Peek().Play();
            }
        }

        public bool IsRunning() {
            // Are tweens being processed?
            return _sequenceQueue.Count > 0;
        }

        public void Clear() {
            // Goodbye. Thanks for your hard work.
            foreach (var sequence in _sequenceQueue) {
                sequence.Kill();
            }

            _sequenceQueue.Clear();
        }

    }

}