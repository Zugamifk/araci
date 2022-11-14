using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshGenerator.Surfaces
{
    public class ProcessStepper
    {
        int _currentStepIndex = 0;
        List<IStep> _steps = new();

        public SurfaceModelBuilder Builder;
        public int CurrentStep => _currentStepIndex;
        public string CurrentStepLabel => _currentStepIndex > 0 && _currentStepIndex < _steps.Count ? _steps[_currentStepIndex].Label : "-";
        public void AddStep(IStep step)
        {
            _steps.Add(step);
        }

        public void AddSteps(IEnumerable<IStep> steps)
        {
            foreach (var s in steps)
            {
                AddStep(s);
            }
        }

        public void StepForward()
        {
            if (_currentStepIndex < _steps.Count)
            {
                _steps[_currentStepIndex].Do(Builder);
                _currentStepIndex++;
            }
        }

        public void StepBack()
        {
            if (_currentStepIndex > 0)
            {
                _currentStepIndex--;
                _steps[_currentStepIndex].Undo(Builder);
            }
        }

        public void Reset()
        {
            _currentStepIndex = 0;
            Builder.Clear();
        }
    }
}
