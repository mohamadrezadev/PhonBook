let btns = document.getElementsByClassName('btn');

for(let btn of btns){
  //add animation to the class
btn.addEventListener('mousedown', function(e){
  btn.children[0].style.left = (e.pageX - btn.offsetLeft) + "px";
  btn.children[0].style.top = (e.pageY - btn.offsetTop) + "px";
  btn.children[0].style.animation = "ripple-expand 0.3s linear";
});

 //remove animation from class after animation duration 
//(0.3secs === 300ms )
btn.addEventListener('mouseup', function(e){
  setTimeout(function(){
     btn.children[0].style.animation = "";
  }, 300);                   
});  
}
