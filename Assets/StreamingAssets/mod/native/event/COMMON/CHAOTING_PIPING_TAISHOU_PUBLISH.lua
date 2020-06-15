options = 
{
    OPTION_1=
    {
        selected = function ()
            gm_data().taishou.prestige = gm_data().taishou.prestige - 10
        end,

        tooltip = function ()
            return {{'TAISHOU_PRESTIGE_CHANGED', -10}}
        end
    }
}