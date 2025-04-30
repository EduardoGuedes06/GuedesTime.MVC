using Microsoft.AspNetCore.Mvc;

public static class ToastHelper
{
    public static void Success(Controller controller, string message)
    {
        controller.TempData["success"] = message;
    }

    public static void Info(Controller controller, string message)
    {
        controller.TempData["info"] = message;
    }

    public static void Warning(Controller controller, string message)
    {
        controller.TempData["warning"] = message;
    }

    public static void Error(Controller controller, string message)
    {
        controller.TempData["error"] = message;
    }
}
