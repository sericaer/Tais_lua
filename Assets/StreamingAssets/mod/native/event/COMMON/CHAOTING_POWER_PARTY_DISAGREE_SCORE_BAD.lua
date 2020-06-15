options = 
{
    OPTION_1=
    {
        selected = function ()
            gm_data().taishou.year_score = 'NORMAL'
            gm_data().chaoting.prestige = gm_data().chaoting.prestige - 5
        end,

        tooltip = function()
            return {{'TAISHOU_YEAR_SCORE', 'NORMAL_SCORE'},
                    {'CHAOTING_PRESTIGE_CHANGED', -5}}
        end
    }
}