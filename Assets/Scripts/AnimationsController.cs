using UnityEngine;
public class AnimationsController : StateMachineBehaviour {
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //if (stateInfo.IsName("Idle")) {
        //    animator.SetBool("jumpDown", false);
        //    animator.SetBool("jumpIdle", false);
        //}        
        //if (stateInfo.IsName("Flying")) {           
        //    PlayerController.instance.isFlying = true;           
        //}
    }
 
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //if (stateInfo.IsName("JumpUp") || stateInfo.IsName("Flying") || stateInfo.IsName("JumpingDown")) {
        //    animator.SetBool("right", false);
        //    animator.SetBool("left", false);
        //}
        //if (stateInfo.IsName("Left")) {           
        //    animator.SetBool("right", false);
        //    animator.SetBool("left", false);
        //}
        //if (stateInfo.IsName("Right")) {         
        //    animator.SetBool("left", false);
        //    animator.SetBool("right", false);
        //}
    }
  
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //if (stateInfo.IsName("Left")) {        
        //    animator.SetBool("left", false);           
        //}
        //if (stateInfo.IsName("Right")) {        
        //    animator.SetBool("right", false);
        //}
    }


}
