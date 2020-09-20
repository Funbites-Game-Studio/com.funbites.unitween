using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UniTween.Tweens;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace UniTween.Tests
{
    public class IntegerTweenTest
    {

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator IntegerTweeWithEnumeratorPasses()
        {
            //SceneManager.LoadScene()
            //IntegerTweenData data = null;
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
