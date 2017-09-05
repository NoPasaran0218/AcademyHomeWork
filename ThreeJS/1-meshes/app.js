var scene=(function () {
    "use strict";
    var scene = new THREE.Scene();
    var renderer = window.WebGLRenderingContext ?
        new THREE.WebGLRenderer({alpha : true}) : new THREE.CanvasRenderer();

    var camera;
    var itemsToRotate = [];
    function InitScene() {
        renderer.setSize(window.innerWidth, window.innerHeight);
        document.getElementById('container').appendChild(renderer.domElement);

        camera = new THREE.PerspectiveCamera(35,window.innerWidth/window.innerHeight,1,1000);

        camera.position.x = -80;
        camera.position.z = 150;
        
        scene.add(camera);
        /*var texture = new THREE.TextureLoader().load( 'earth.jpg');
        var material2 = new THREE.MeshBasicMaterial({map:texture});

        var sphere = new THREE.Mesh(new THREE.SphereGeometry(40,64,64), material2);
        sphere.position.x=-90;
        sphere.rotation.x=0.3;
        itemsToRotate.push(sphere);
        scene.add(sphere);

        render();*/
        var loader = new THREE.TextureLoader();
        loader.load('earth.jpg', function(texture){
            
            var material2 = new THREE.MeshBasicMaterial({map:texture});
            var sphere = new THREE.Mesh(new THREE.SphereGeometry(40,64,64), material2);
            sphere.position.x=-90;
            sphere.rotation.x=0.3;
            itemsToRotate.push(sphere);
            scene.add(sphere);

            render();
        });
    }

    function render() {
        for(var i=0; i<itemsToRotate.length;++i){
            itemsToRotate[i].rotation.y+=0.01;
        }
       renderer.render(scene, camera);
       requestAnimationFrame(render);
    }

    return{
        initScene: InitScene
    }

})();