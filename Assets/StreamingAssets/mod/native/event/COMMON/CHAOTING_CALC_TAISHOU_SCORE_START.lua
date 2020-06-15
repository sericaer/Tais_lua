trigger = function()
    return gm_data().date.month == 12 and gm_data().date.day == 20
end

options = 
{
    OPTION_1=
    {
        selected = function ()
        end,

        next_event = function ()
            local tax = gm_data().chaoting.year_report_tax - gm_data().chaoting.year_expext_tax
            local pop = gm_data().chaoting.year_report_pop - gm_data().chaoting.pre_report_pop
            
            if tax > 0 and pop > 0 then
                return 'CHAOTING_CALC_TAISHOU_SCORE_GOOD'
            end
            if tax < 0 and pop < 0 then
                return 'CHAOTING_CALC_TAISHOU_SCORE_BAD'
            end
            return 'CHAOTING_CALC_TAISHOU_SCORE_NORMAL'
        end
    }
}