# BonjourMirrorIOS
Mirror Discovery using Bonjour over Wifi for IOS

IOS 14 has changed privacy and networking such that [Mirror Network Discovery](https://mirror-networking.gitbook.io/docs/components/network-discovery) does not seem to work on my iphone.  To deal with this, I wanted to use Bonjour, which is not restricted.  After finding code examples for the server and client as Unity plugins, I was able to get that to work.

There is BuildPostProcessor script in the Editor folder that adds the Privacy - Local Network Usage Description to the pinfo list, but the code for adding the Bonjour services does not work.  Instead, you must manually add the Bonjour services and the place the string \_bonjourmirror.\_tcp into the first position in the array. 

Even with the privacy element in the plist, [the phone does not always ask the user to accept the use of the local network](https://stackoverflow.com/questions/64308595/how-to-trigger-the-local-network-dialog-authorization-for-multicast-entitlement). I have added some code connected to the Privacy button to send dummy packets to trigger the dialogue.  It works, but sometimes only after you have reset privacy in all apps in the phone.

The present code uses bonjour to do multiplayer discovery for Mirror in Unity.  The present code will allow the host system to create their player and broadcast their service over bonjour.  When the client can connect and Mirror's Network manager is called to spawn the client player.  This is not a real game, so I have not worked on making the client movements mirrored on the server.

On phones with cellular data, the ip address for the server changes depending on whether cellular is on or not.  The present code addresses that by using Sockets to get the local ip address (GetLocalIPAddressSockets). 

It took me forever to sort this stuff out, so I am making this code available to help anyone else.

The Bonjour client code is from the [Unity Bonjour Browser example at the bottom of this page](https://docs.unity3d.com/2019.4/Documentation/Manual/PluginsForIOS.html).

The Bonjour Broadcast code is from [UnityBonjourNativePluginOSX](https://github.com/naojitaniguchi/UnityBonjourNativePluginOSX).


