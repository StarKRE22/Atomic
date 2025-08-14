using System;
using Atomic.Entities;
using UnityEngine;

namespace DefaultNamespace
{
    public class Sample : MonoBehaviour
    {
        private void Awake()
        {
            Entity entity1 = new Entity("Vasya");
            entity1.AddTag("Character");
            Entity entity3 = new Entity("Vasya");
            Entity entity2 = new Entity("Petya");
            EntityCollection entityCollection = new EntityCollection(); //HashSet & Linked List
            entityCollection.Add(entity1);

            
            
            EntityFilter filter = new EntityFilter(entityCollection, e => e.Name == "Vasya");

            EntityFilter characterFiler = new EntityFilter(filter, e => e.HasTag("Character"));
            
            
            foreach (IEntity entity in filter)
            {
                
            }
        }
    }
}