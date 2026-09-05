# king-down-stairs
一个简单的Unity 2D 下楼梯小游戏
在这个游戏中你将扮演“国王”，进入无穷无尽的天空世界，请注意：不要踩到砖块哟~

## 目录结构

```
Assets/
├── Animations/    # 角色动画（行走、受伤等）
├── Audios/        # 音效与背景音乐
├── Image/         # 图片素材
├── Prefabs/       # 预制体（阶梯等）
├── Scenes/        # 场景（SampleScene）
├── Scrips/        # 游戏脚本
└── TextMesh Pro/  # 文本组件
```

## 玩法说明

- 使用 **A / D** 键左右移动角色。
- 阶梯会不断向上移动，站在阶梯上角色会随之上升。
- 踩中 **Normal** 阶梯：恢复 1 点生命值（上限 10）。
- 踩中 **Nails** 钉子阶梯：扣除 3 点生命值并触发受伤动画与音效。
- 撞到 **Ceil** 天花板：扣除 3 点生命值。
- 每存活 2 秒增加 1 分。
- 生命值归 0 或掉落到 **DeathLine** 死亡线以下时游戏结束，显示重玩按钮。

## 脚本说明

| 脚本 | 功能 |
|------|------|
| `Player.cs` | 控制角色移动、血量、计分、碰撞判定、死亡与重玩逻辑 |
| `Floor.cs` | 控制阶梯向上移动，超出范围后销毁并生成新阶梯 |
| `FloorManager.cs` | 随机生成阶梯预制体并设置其位置 |

### Player.cs

- `moveSpeed`：角色移动速度。
- `Hp` / `HpBar`：当前血量与血条（由子物体显示/隐藏实现）。
- `score` / `scoreText`：分数与分数显示文本。
- `OnCollisionEnter2D`：处理与阶梯（Normal / Nails）、天花板（Ceil）的碰撞。
- `OnTriggerEnter2D`：处理与死亡线（DeathLine）的触发。
- `Die()`：暂停游戏并显示重玩按钮。
- `Replay()`：恢复时间流速并重新载入场景。

### Floor.cs

- `moveSpeed`：阶梯上升速度。
- 当阶梯 y 坐标超过 6.5 时，销毁自身并调用 `FloorManager.SpawnFloor()` 生成新阶梯。

### FloorManager.cs

- `floorPrefabs`：阶梯预制体数组。
- `SpawnFloor()`：随机选择一个预制体，在随机水平位置（-3.4 ~ 4.3）、y = -5 处生成，并作为 FloorManager 的子物体。

## 标签（Tags）约定

| Tag | 用途 |
|-----|------|
| `Normal` | 普通阶梯（加血） |
| `Nails` | 钉子阶梯（扣血） |
| `Ceil` | 天花板（扣血） |
| `DeathLine` | 死亡线（触发死亡） |

## 使用说明

1. 使用 Unity 打开本项目。
2. 打开 `Assets/Scenes/SampleScene.unity` 场景。
3. 点击播放按钮运行游戏。

## 依赖

- Unity（建议 2019.4 或更高版本）
- TextMesh Pro
