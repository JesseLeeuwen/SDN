# Simple Data Notation (SDN)

Simple Data Notation started as a project to create some library that could be used 
in small projects. The initial setup for sdn was a simple setup without nesting. This is a more robust version with nesting.

##SDN format
```javascript
data: [
    "This is a piece of text",
    "this is a piece of text"
]

scene: [
    name: [menu]
    objects: [
        player: [
            name: [player1]
            lvl: [1]
            str: [10]
            int: [7]
            dex: [9]
            hp: [14]
            attack: [4]
            inventory: [10, 20, 33, 120]
        ]
        frog_enemy: [
            name: [frog]
            lvl: [2]
            hp: [4]
            attack: [4]
        ]
    ]
]
```