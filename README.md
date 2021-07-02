# BonjourMirrorIOS
Mirror Discovery using Bonjour over Wifi for IOS

IOS 14 has changed the networking such that Mirror Network Discovery does not seem to work.  To deal with this, I wanted to use Bonjour, which is not restricted.  After finding code examples for the server and client as Unity plugins, I was able to get that to work.

The present code uses bonjour to do multiplayer discovery for Mirror in Unity.  The present code will allow the host system to create their player and broadcast their service over bonjour.  When the client can connect and Mirror's Network manager is called to spawn the client player.  

There is BuildPostProcessor script in the Editor folder that adds the Privacy - Local Network Usage Description to the pinfo list, but the code for adding the Bonjour services does not work.  Instead, you must manually add the Bonjour services and the place the string \_bonjourmirror.\_tcp into the first position in the array. 

On phones with cellular data, the ip address for the server changes depending on whether cellular is on or not.  The present code addresses that by using Sockets to get the local ip address (GetLocalIPAddressSockets). 

The Bonjour client code is from here.
https://docs.unity3d.com/2019.4/Documentation/Manual/UnityasaLibrary-iOS.html

The Bonjour Broadcast code is from here.
https://github.com/naojitaniguchi/UnityBonjourNativePluginOSX


