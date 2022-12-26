using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    public interface ICookable
    {
        public CookState CookState { get; set; }
    }
}