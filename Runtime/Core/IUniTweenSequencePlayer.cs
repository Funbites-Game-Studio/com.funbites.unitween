using System.Collections.Generic;
using UnityEngine;

namespace UniTween.Core {
    public interface IUniTweenSequencePlayer {
        string DisplayName {
            get;
        }

        IEnumerable<UniTweenSequence> Tweens { get; }

        GameObject gameObject { get; }
    }
}
