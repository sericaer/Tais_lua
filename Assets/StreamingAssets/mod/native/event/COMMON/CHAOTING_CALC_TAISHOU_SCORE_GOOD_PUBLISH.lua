options = 
{
    OPTION_1=
    {
        selected = function ()
            gm_data().taishou.year_score = 'GOOD'
        end,

        next_event = function ()
            return 'CHAOTING_BIAOYANG_TAISHOU_PUBLISH'
        end,

        tooltip = function ()
            return {{'TAISHOU_YEAR_SCORE_RESULT', 'GOOD_SCORE'}}
        end
    }
}