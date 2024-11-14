using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts
{
    public class Waypoints : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] wayPoints;
        public GameObject[] WayPoints { get => wayPoints; set => wayPoints = value; }
    }
}
