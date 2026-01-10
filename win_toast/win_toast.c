// win_toast.cpp : Defines the exported functions for the DLL.
//

#define INITGUID
#define COBJMACROS
#define WIN32_LEAN_AND_MEAN

#include "framework.h"
#include "win_toast.h"

#ifdef _DEBUG
#define Assert(Cond) do { if (!(Cond)) __debugbreak(); } while (0)
#else
#define Assert(Cond) ((void)(Cond))
#endif

#define HR(hr) do { HRESULT _hr = (hr); Assert(SUCCEEDED(_hr)); } while (0)

// https://github.com/mmozeiko/TwitchNotify/
#include "WindowsToast.h"

WINTOAST_API int fnToastSimple(wchar_t* notifyAppId, wchar_t* notifyName, wchar_t* xmlText)
{
    BOOL result = FALSE;
    size_t length = wcslen(xmlText);

    {
        WindowsToast toast;
        memset(&toast, 0, sizeof(WindowsToast));
        WindowsToast_Init(&toast, notifyName, notifyAppId);
        WindowsToast_HideAll(&toast, notifyAppId);

        result = WindowsToast_ShowSimple(&toast, xmlText, (int)length, NULL, 0);

        WindowsToast_Done(&toast);
    }

    return result;
}
