using UnityEngine;

public class DummyAnimationController : AnimationController{
    // Consts
    private const string HIT_TRIGGER = "hit_param";

    // Attributes
    private readonly Dummy dummy;

    // Constructor
    public DummyAnimationController(Dummy dummy){
        this.dummy = dummy;
        model = dummy.transform.Find("Model");
        animator = model.GetComponent<Animator>();
        meshRenderer = model.GetComponent<MeshRenderer>();

        dummy.OnDamagedEvent += IndicateDamaged;
        dummy.stateController.OnDamageDelayOverEvent += IndicateUnamaged;
    }

    // Functions
    private void IndicateDamaged(){
        meshRenderer.material.color = Color.red;
    }

    private void IndicateUnamaged(){
        meshRenderer.material.color = Color.white;
    }
}