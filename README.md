# Quest-Dialogue-System
Dialogue &amp;&amp; Dialogue System

Resources Link

Google Drive: https://drive.google.com/file/d/1vrwcYDxFpUffSiIdxshEABpzJUY8XcY4/view?usp=sharing

Baidu Cloud: TAB

解决的问题：

【问题1】
DM脚本调用了Talkable、QuestTarget、Questable类型，脚本之间的耦合性太乱糟糟
【解决】
* DM脚本只保留Talkable类型，即可说话的、可打开对话的这个脚本
* 其余的QuestTarget、Questable类型全部通过Talkable脚本进行调用

【问题2】
场景切换过程中，NPC不能够保存任务是否完成
【解决】
场景转换保存加载场景1中NPC任务状态和isFinished
* 在离开场景1到场景2之前的SceneTransition脚本中SaveData保存数据
* 在回到场景1时的所有Questable脚本加载数据
（并不包含游戏退出以后再运行时的保存和加载）

添加：
1. 【方法和脚本】添加必要的注释
2. 从重要度来说，DM脚本相挂钩的核心脚本只有【Talkable脚本】
    1. 「QuestTarget」和「Questable」脚本在DM脚本中都是通过【Talkable类型】来进行访问
3. 任务列表打开状态时，打开对话窗口会将任务列表关闭
4. 任务完成后的奖励（经验值和金币）
5. 在玩家收集物品，和委派收集类任务的NPC对话时，都会检测当前的收集类任务是否完成（之前是只有和NPC对话时才检测）
6. 场景转换过程中保存和加载NPC有关任务状态等参数
    1. 其实就是在【Questable脚本】中添加SaveData和LoadData方法

潜在问题：
1.脚本的耦合性太强
（事件与委托日后解决）

2.PlatformEffector
（这个Demo中不会出现，只是项目中的OnewayPlatform脚本无法解决这类情况）

3.文字滚动与Rich Tex“不和谐”
（小伙伴「蓝渡丶」的解决方法：这里我选取了留言：先做个判断，有无富文本的标识符，有的话把这段文字遍历一遍，记下开头以及结束的位置，然后单独把这部分内容一次性显示出来。如果更进一步的话，把富文本标识提取出来，富文本标识内的每个字单独加个富文本标记进行输出，也可以让富文本做到逐字输出的效果，就是比较蛮烦，效率估计也不高）
