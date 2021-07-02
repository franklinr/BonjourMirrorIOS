/*
	This is the code for starting a bonjour service from this git.
 https://github.com/naojitaniguchi/UnityBonjourNativePluginOSX
*/

/*#include "BonjourPlugin.pch"*/
#include "BonjourServer.h"

extern "C" {

void _startService(char *type, char *name, int port, char *domain){
    BonjourServer* bonjourServer = [BonjourServer sharedManager];
    
    // サーバーからサービスをスタート
    NSString *typeString = [NSString stringWithCString: type encoding:NSUTF8StringEncoding];
    NSString *nameString = [NSString stringWithCString: name encoding:NSUTF8StringEncoding];
    NSString *domainString = [NSString stringWithCString: domain encoding:NSUTF8StringEncoding];
    
    [bonjourServer startService:typeString name:nameString port:port domain:domainString];
}


void _stopService(){
    BonjourServer* bonjourServer = [BonjourServer sharedManager];
    [bonjourServer stopService];
}

}

