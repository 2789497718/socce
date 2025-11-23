using Godot;

using Godot.Collections;
using Soccer.Characters;

public static partial   class   KeyUtils 
{
    public enum Action {LEFT, RIGHT, UP, DOWN, SHOOT, PASS}

        
    // --- 【核心改进】 ---
    // 1. 重命名变量：避免使用 "Dictionary"，因为它与 C# 的类型名冲突。
    //    "ControlMappings" 或 "InputMaps" 是更好的名字。
    // 2. 使用 readonly：让这个字典在初始化后不能被意外地整个替换掉，更安全。
    // 3. 修正初始化语法：每个条目都是 { key, value } 的形式。
    // 4. 使用 StringName：这是 Godot 为动作名、节点名等优化的字符串类型。
    public static readonly Dictionary<ControlScheme, Dictionary<Action, StringName>> ControlMappings = new()
    {
        // --- 玩家1 (P1) 的按键映射 ---
        {
            ControlScheme.P1, new Dictionary<Action, StringName>
            {
                { Action.LEFT, "p1_left" },
                { Action.RIGHT, "p1_right" },
                { Action.UP, "p1_up" },
                { Action.DOWN, "p1_down" },
                { Action.SHOOT, "p1_shoot" },
                { Action.PASS, "p1_pass" }
            }
        },

        // --- 玩家2 (P2) 的按键映射 ---
        {
            ControlScheme.P2, new Dictionary<Action, StringName>
            {
                { Action.LEFT, "p2_left" },
                { Action.RIGHT, "p2_right" },
                { Action.UP, "p2_up" },
                { Action.DOWN, "p2_down" },
                { Action.SHOOT, "p2_shoot" },
                { Action.PASS, "p2_pass" }
            }
        },
    };
    
    // --- 【新增的 Vector2 方法】 ---
    /// <summary>
    /// 根据指定的控制方案，获取一个标准化的二维移动向量。
    /// </summary>
    /// <param name="scheme">玩家的控制方案 (P1, P2, etc.)</param>
    /// <returns>一个标准化的 Vector2，代表输入方向。</returns>
    public static Vector2 GetMovementVector(ControlScheme scheme)
    {
  
        // 1. 从我们的字典中获取四个方向的动作名
        StringName left = GetActionString(scheme, Action.LEFT);
        StringName right = GetActionString(scheme, Action.RIGHT);
        StringName up = GetActionString(scheme, Action.UP);
        StringName down = GetActionString(scheme, Action.DOWN);

        // 2. 使用 Godot 内置的 Input.GetVector 方法
        // 这个方法会自动处理输入强度（支持手柄摇杆）和归一化（防止斜向移动速度过快）
        // 参数顺序: negative_x, positive_x, negative_y, positive_y
        Vector2 vector2 = Input.GetVector(left, right, up, down);
        return vector2;
    }
    
    
     /// <summary>
        /// 获取指定控制方案和动作对应的 Godot 输入动作名。
        /// </summary>
        public static StringName GetActionString(ControlScheme scheme, Action action)
        {
            if (ControlMappings.TryGetValue(scheme, out var actionMap))
            {
                if (actionMap.TryGetValue(action, out var actionName))
                {
                    return actionName;
                }
            }
            GD.PrintErr($"错误: 未能找到控制方案 '{scheme}' 的动作 '{action}' 的映射!");
            return null;
        }

        public static bool IsActionJustPressed(ControlScheme scheme, Action action)
        {
            return Input.IsActionJustPressed(GetActionString(scheme,action));
        }
}
