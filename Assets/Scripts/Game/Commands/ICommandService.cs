using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommandService
{
    void DoCommand(ICommand command);
}
