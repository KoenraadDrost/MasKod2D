using MasKod2D.entity;
using MasKod2D.GraphFromBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasKod2D.behaviour
{
    public class PathFollowingBehaviour : SteeringBehaviour
    {
        //public List<Node> Path { get; set; }
        public Node WayPoint { get; set; }
        
        public PathFollowingBehaviour(MovingEntity me, Node wayPoint) : base(me) 
        {
            WayPoint = wayPoint;
        }

        public override Vector2D Calculate()
        {
            ME.MyWorld.Target.Pos = new Vector2D(WayPoint.Location.X * 100, WayPoint.Location.Y * 100);
            ME.MaxSpeed = 20;
            ME.SB = new SeekBehaviour(ME);
            return ME.SB.Calculate();
        }

        
    }
}
