//
//  BonjourServer.h
//  BonjourPlugin
//
//  Created by 谷口 直嗣 on 2014/03/24.
//  Copyright (c) 2014年 谷口 直嗣. All rights reserved.
//
#import <Foundation/Foundation.h>

@interface BonjourServer : NSObject{
    NSNetService *netService;
}

+ (BonjourServer*)sharedManager ;
+ (id)allocWithZone:(NSZone *)zone ;
- (id)copyWithZone:(NSZone*)zone ;
- (id)retain ;
- (oneway void)release;
- (id)autorelease;

-(void)startService:(NSString*)type name:(NSString*)name port:(int)port domain:(NSString*)domain;
-(void)stopService;

@end
