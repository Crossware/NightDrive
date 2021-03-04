using UnityEngine;

interface Car
{
    GameObject carObject{ get; set; }
    Vector3 Position{ get; set; }
    float Speed{ get; set; }
    bool deleted { get; set; }

    void spawn();
    void spawn(Vector3 position);
    void despawn();
    void update();
}