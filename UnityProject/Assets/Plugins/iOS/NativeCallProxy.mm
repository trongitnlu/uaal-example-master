#import <Foundation/Foundation.h>
#import "NativeCallProxy.h"


@implementation FrameworkLibAPI

id<NativeCallsProtocol> api = NULL;
+(void) registerAPIforNativeCalls:(id<NativeCallsProtocol>) aApi
{
    api = aApi;
}

@end


extern "C" {
    void sendMessageToMobileApp(const char* color) { return [api sendMessageToMobileApp:[NSString stringWithUTF8String:color]]; }
}

